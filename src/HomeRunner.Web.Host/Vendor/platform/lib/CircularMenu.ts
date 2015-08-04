
module SlidingApps.JQuery.PlugIn.CircularMenu {

    export class Guid {
        public static NewGuid(): string {
            return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c: string) => {
                var r: number = Math.random() * 16 | 0;
                var v: any = (c == 'x') ? r : r & 0x3 | 0x8;

                return v.toString(16);
            });
        }
    }

    export enum SelectionMode {
        ZERO_OR_ONE = <any>'ZERO_OR_ONE',
        EXACTLY_ONE = <any>'EXACTLY_ONE',
        ZERO_OR_MORE = <any>'ZERO_OR_MORE'
    }

    export interface ICircularMenuData {
        name: string;
        label?: string;
        value?: number;
        isSelected?: boolean;
        backgroundColor?: string;
        backgroundColorSelected?: string;
    }

    export class CircularMenuData implements ICircularMenuData {

        constructor(data?: ICircularMenuData) {
            if (data) {
                this.name = data.name ? data.name : this.name;
                this.label = data.label ? data.label : this.label;
                this.value = data.value ? data.value : this.value;
                this.isSelected = data.isSelected ? data.isSelected : this.isSelected;
                this.backgroundColor = data.backgroundColor ? data.backgroundColor : this.backgroundColor;
                this.backgroundColorSelected = data.backgroundColorSelected ? data.backgroundColorSelected : this.backgroundColorSelected;
            }
        }

        public name: string = 'item';
        public label: string = '';
        public value: number = 0;
        public isSelected: boolean = false;
        public backgroundColor: string = '#717171';
        public backgroundColorSelected: string = '#258bbc';
    }

    export class Size {
        constructor(width?: number, height?: number) {
            this.width = width;
            this.height = height;
        }

        public width: number;
        public height: number;
    }

    export class Position {
        constructor(x?: number, y?: number) {
            this.x = x;
            this.y = y;
        }

        public x: number;
        public y: number;
    }

    export interface ISeries {
        innerSerie?: ICircularMenuData;
        outerSerie?: Array<ICircularMenuData>;
    }

    export class Series {

        constructor(series?: ISeries) {
            if (series) {
                this.innerSerie = series.innerSerie ? series.innerSerie : this.innerSerie;
                this.outerSerie = series.outerSerie ? series.outerSerie : this.outerSerie;
            }

            if (this.innerSerie) {
                this.innerSerie = $.extend(true, this.innerSerie, new CircularMenuData(this.innerSerie));
            }

            if (this.outerSerie) {
                for (var i: number = 0; i < this.outerSerie.length; i++) {
                    this.outerSerie[i] = $.extend(true, this.outerSerie[i], new CircularMenuData(this.outerSerie[i]));
                }
            }
        }

        public innerSerie: ICircularMenuData = null;
        public outerSerie: Array<ICircularMenuData> = new Array < ICircularMenuData>();
    }

    export interface ICircularMenuPublicOptions {
        mode?: SelectionMode;
        size?: Size;
        series?: ISeries;
    }

    export interface ICircularMenuPrivateOptions {
        radius?: number;
        labelRadius?: number;
    }

    export interface ICircularMenuOptions extends ICircularMenuPublicOptions, ICircularMenuPrivateOptions {
        
    }

    export class Vector {
        public svg: D3.Selection;
        public circleGroup: D3.Selection;
        public circle: D3.Selection;
        public donut: D3.Layout.PieLayout;
        public arc: D3.Svg.Arc;
        public arcGroup: D3.Selection;
    }

    export class MenuItemSelectorFactory {

        public static create(mode: SelectionMode): MenuItemSelector {
            var instance: MenuItemSelector = null;
            switch (mode) {
                case SelectionMode.ZERO_OR_ONE:
                    instance = new ZeroOrOneMenuItemSelector();
                    break;

                case SelectionMode.EXACTLY_ONE:
                    instance = new ExactlyOneMenuItemSelector();
                    break;

                case SelectionMode.ZERO_OR_MORE:
                    instance = new ZeroOrMoreMenuItemSelector();
                    break;

                default:
                    throw new Error('NOT IMPLEMENTED');
                    break;
            }

            return instance;
        }

    }

    export class MenuItemSelector {       
        public setSelection(vector: Vector, toggledItem: ICircularMenuData, options: ICircularMenuOptions): void { throw new Error('ABSTRACT METHOD'); }
    }

    export class ZeroOrOneMenuItemSelector extends MenuItemSelector {

        public setSelection(vector: Vector, toggledItem: ICircularMenuData, options: ICircularMenuOptions): void {

            var i: number;
            if (options.series.innerSerie == toggledItem) {
                toggledItem.isSelected = !toggledItem.isSelected;

                for (i = 0; i < options.series.outerSerie.length; i++) {
                    options.series.outerSerie[i].isSelected = false;
                }
            } else {
                if (toggledItem.isSelected) {
                    toggledItem.isSelected = !toggledItem.isSelected;
                } else {
                    options.series.innerSerie.isSelected = false;
                    for (i = 0; i < options.series.outerSerie.length; i++) {
                        options.series.outerSerie[i].isSelected = false;
                    }

                    toggledItem.isSelected = !toggledItem.isSelected;
                }
            }
        }
    }

    export class ExactlyOneMenuItemSelector extends MenuItemSelector {

        public setSelection(vector: Vector, toggledItem: ICircularMenuData, options: ICircularMenuOptions): void {
            options.series.innerSerie.isSelected = false;
            for (var i = 0; i < options.series.outerSerie.length; i++) {
                options.series.outerSerie[i].isSelected = false;
            }

            toggledItem.isSelected = !toggledItem.isSelected;
        }
    }

    export class ZeroOrMoreMenuItemSelector extends MenuItemSelector {

        public setSelection(vector: Vector, toggledItem: ICircularMenuData, options: ICircularMenuOptions): void {
            toggledItem.isSelected = !toggledItem.isSelected;
        }
    }

    export class CircularMenuPlugIn {
        public static PLUG_IN_NAME: string = 'circularMenu';

        constructor($element: JQuery, options: ICircularMenuPublicOptions) {
            this.$element = $element;
            this.options = options;

            this.init();
        }

        private id: string = Guid.NewGuid();
        private $element: JQuery;
        private options: ICircularMenuOptions;
        private defaults: ICircularMenuOptions = {
            mode: SelectionMode.ZERO_OR_ONE,
            data: [],
            size: new Size(500, 500),
            radius: 0
        };

        private vector: Vector = new Vector();
        private menuItemSelector: MenuItemSelector;

        private init(): void {
            this.options = $.extend(true, this.defaults, this.options);

            this.options.radius = (Math.min(this.options.size.width, this.options.size.height) / 2 - 50) * 0.9;
            this.options.labelRadius = (Math.min(this.options.size.width, this.options.size.height) / 2 - 50) * 0.9;

            this.menuItemSelector = MenuItemSelectorFactory.create(this.options.mode);
        }


        public draw(options: ICircularMenuOptions): JQuery {
            this.options = $.extend(true, this.options, options);
            this.options.series = new Series(this.options.series);

            this.createVector();

            return this.$element;
        }

        private createVector(): void {
            this.$element.addClass('app-circular-menu');

            this.vector.svg =
                d3.select(this.$element[0])
                    .append('svg:svg')
                    .data([this.options.series.outerSerie])
                    .attr('id', 'vector_' + this.id)
                    .attr('width', this.options.size.width)
                    .attr('height', this.options.size.height);

            if (this.options.series.innerSerie) {
                this.createInnerSerie();
            }

            if (this.options.series.outerSerie) {
                this.createOuterSerie();
            }
        }

        private createInnerSerie(): void {
            this.createInnerSerieCircle();
            this.createInnerSerieValueText();
            this.createInnerSerieLabel();
        }

        private createInnerSerieCircle(): void {
            this.vector.circleGroup =
                this.vector.svg.selectAll("g.circle")
                .data([this.options.series.innerSerie])
                .enter()
                .append('svg:g')
                .attr('id', this.options.series.innerSerie.name + '_circle_group_' + this.id)
                .attr('class', 'circle')
                .style('cursor', 'pointer')
                .on('click', (event, index) => {
                    this.menuItemSelector.setSelection(this.vector, this.options.series.innerSerie, this.options);

                    this.renderInnerSerieDataItem(this.options.series.innerSerie);
                    for (var i: number = 0; i < this.options.series.outerSerie.length; i++) {
                        this.renderOuterSerieDataItem(this.options.series.outerSerie[i]);
                    }
                });

            this.vector.circleGroup
                .append("circle")
                .attr('id', this.options.series.innerSerie.name + '_circle_' + this.id)
                .attr('fill', this.options.series.innerSerie.backgroundColor)
                .attr("r", (this.options.radius * 0.75) / 2)
                .attr("cx", this.options.radius + 75)
                .attr("cy", this.options.radius + 75);
        }

        private createInnerSerieValueText(): void {
            this.vector.circleGroup
                .append("svg:text")
                .attr('id', this.options.series.innerSerie.name + '_value_' + this.id)
                .attr('x', this.options.radius + 75)
                .attr('y', this.options.radius + 70)
                .attr('text-anchor', 'middle')
                .attr('class', 'noselect value-text')
                .text(this.options.series.innerSerie.label);
        }

        private createInnerSerieLabel(): void {
            this.vector.circleGroup
                .append("svg:text")
                .attr('id', this.options.series.innerSerie.name + '_label_' + this.id)
                .attr('x', this.options.radius + 75)
                .attr('y', this.options.radius + 105)
                .attr('text-anchor', 'middle')
                .attr('class', 'noselect label')
                .text(this.options.series.innerSerie.name);
        }

        private createOuterSerie(): void {
            this.createOuterSerieDonut();
            this.createOuterSerieArcs();
            this.createOuterSerieValueText();
            this.createOuterSerieLabel();
        }

        private createOuterSerieDonut(): void {
            this.vector.donut = d3.layout.pie().startAngle(-30 * (Math.PI / 180)).endAngle(330 * (Math.PI / 180));
        }

        private createOuterSerieArcs(): void {
            this.vector.arc = d3.svg.arc().innerRadius(this.options.radius * .5).outerRadius(this.options.radius);
            this.vector.arcGroup = this.vector.svg.selectAll('g.arc')
                .data(this.vector.donut.value(d => d.value))
                .enter()
                .append('svg:g')
                .attr('id', d => d.data.name + '_arc_group_' + this.id)
                .attr('class', 'arc')
                .attr('transform', 'translate(' + (this.options.radius + 75) + ',' + (this.options.radius + 75) + ')')
                .style('cursor', 'pointer')
                .on('click', (event, index) => {
                    this.menuItemSelector.setSelection(this.vector, event.data, this.options);

                    this.renderInnerSerieDataItem(this.options.series.innerSerie);
                    for (var i: number = 0; i < this.options.series.outerSerie.length; i++) {
                        this.renderOuterSerieDataItem(this.options.series.outerSerie[i]);
                    }
                });

            this.vector.arcGroup.append('svg:path')
                .attr('id', d => d.data.name + '_arc_' + this.id)
                .attr('fill', d => d.data.backgroundColor)
                .attr('d', this.vector.arc);
        }

        private createOuterSerieValueText(): void {
            this.vector.arcGroup.append("svg:text")
                .attr('id', d => { return d.data.name + '_value_' + this.id; })
                .attr('transform', d => {
                    var centroid: any = this.vector.arc.centroid(d);
                    var position: Position = new Position(centroid[0], centroid[1]);
                    var height = Math.sqrt(position.x * position.x + position.y * position.y);
                    var correction: Position = new Position(0, (position.y / height * (this.options.labelRadius - 50)) < 0 ? -8 : 16);

                    return 'translate(' + (position.x / height * (this.options.labelRadius + correction.x - this.options.radius * 0.3)) + ',' + (position.y / height * (this.options.labelRadius + correction.y - this.options.radius * 0.3)) + ')';
                })
                .attr('text-anchor', d => {
                    return (d.endAngle + d.startAngle) / 2 > Math.PI ? 'middle' : 'middle';
                })
                .attr('class', 'noselect value-text')
                .text(d => {
                    return d.data.label;
                });
        }

        private createOuterSerieLabel(): void {
            this.vector.arcGroup.append("svg:text")
                .attr('id', d => { return d.data.name + '_label_' + this.id; })
                .attr('transform', d => {
                    var centroid: any = this.vector.arc.centroid(d);
                    var position: Position = new Position(centroid[0], centroid[1]);
                    var height = Math.sqrt(position.x * position.x + position.y * position.y);
                    var correction: Position = new Position(0, (position.y / height * (this.options.labelRadius - 50)) < 0 ? -24 : -9);

                    return 'translate(' + (position.x / height * (this.options.labelRadius + correction.x + 40)) + ',' + (position.y / height * (this.options.labelRadius + correction.y + 40)) + ')';
                })
                .attr('style', d => { return 'fill: ' + d.data.backgroundColor + ';'; })
                .attr('class', 'noselect label')
                .attr('text-anchor', d => {
                    return ((d.endAngle + 0.0) + (d.startAngle + 0.0)) / 2 > Math.PI ? 'middle' : 'middle';
                })
                .text(d => {
                    return d.data.name;
                });
        }

        private renderInnerSerieDataItem(dataItem: ICircularMenuData): void {
            d3.select('#' + dataItem.name + '_circle_group_' + this.id).select('circle')
                .transition()
                .duration(300)
                .attr("fill", dataItem.isSelected ? dataItem.backgroundColorSelected : dataItem.backgroundColor)
                .attr("r", (this.options.radius * 0.75 * (dataItem.isSelected ? 1.15 : 1)) / 2);
        }

        private renderOuterSerieDataItem(dataItem: ICircularMenuData): void {
            var innerFactor = dataItem.isSelected ? 0.6 : 0.5;
            var outerFactor = dataItem.isSelected ? 1.1 : 1.0;
            var textFactor = dataItem.isSelected ? 0.6 : 1;

            d3.select('#' + dataItem.name + '_arc_group_' + this.id).select('path')
                .transition()
                .duration(300)
                .attr("fill", (d, i) => { return dataItem.isSelected ? dataItem.backgroundColorSelected : dataItem.backgroundColor; })
                .attr("d", d3.svg.arc().innerRadius(this.options.radius * innerFactor).outerRadius(this.options.radius * outerFactor));

            d3.select('#' + dataItem.name + '_value_' + this.id)
                .transition()
                .duration(300)
                .attr("transform", d => {
                    var centroid: any = this.vector.arc.centroid(d);
                    var position: Position = new Position(centroid[0], centroid[1]);
                    var height = Math.sqrt(position.x * position.x + position.y * position.y);
                    var correction: Position = new Position(0, (position.y / height * (this.options.labelRadius - 50)) < 0 ? -8 : 16);

                    return "translate(" + (position.x / height * (this.options.labelRadius + correction.x - this.options.radius * 0.3 * textFactor)) + ',' + (position.y / height * (this.options.labelRadius + correction.y - this.options.radius * 0.3 * textFactor)) + ")";
                });

            d3.select('#' + dataItem.name + '_label_' + this.id)
                .attr("style", "font-family: Arial; fill:" + (dataItem.isSelected ? dataItem.backgroundColorSelected : dataItem.backgroundColor) + "; font-size: 18px; font-weight: bold;")
                .transition()
                .duration(300)
                .attr("transform", d => {
                    var centroid: any = this.vector.arc.centroid(d);
                    var position: Position = new Position(centroid[0], centroid[1]);
                    var height = Math.sqrt(position.x * position.x + position.y * position.y);
                    var correction: Position = new Position(0, (position.y / height * (this.options.labelRadius - 50)) < 0 ? -24 : -9);

                    return "translate(" + (position.x / height * (this.options.labelRadius + correction.x + (40 * (1 / textFactor)))) + ',' + (position.y / height * (this.options.labelRadius + correction.y + 40 * (1 / textFactor))) + ")";
                });
        }
    }
}

interface JQuery {
    circularMenu(action: string, options: SlidingApps.JQuery.PlugIn.CircularMenu.ICircularMenuPublicOptions): JQuery;
}

(($, window, document, undefined) => {
    $.fn[SlidingApps.JQuery.PlugIn.CircularMenu.CircularMenuPlugIn.PLUG_IN_NAME] = function (action: string, options: SlidingApps.JQuery.PlugIn.CircularMenu.ICircularMenuPublicOptions) {

        return this.each(function (index: number, element: JQuery) {

            var instance: SlidingApps.JQuery.PlugIn.CircularMenu.CircularMenuPlugIn = null;
            if (!$.data(this, 'JQuery_' + SlidingApps.JQuery.PlugIn.CircularMenu.CircularMenuPlugIn.PLUG_IN_NAME)) {
                instance = new SlidingApps.JQuery.PlugIn.CircularMenu.CircularMenuPlugIn($(this), options);
                $.data(this, 'JQuery_' + SlidingApps.JQuery.PlugIn.CircularMenu.CircularMenuPlugIn.PLUG_IN_NAME, instance);
            }
            else {
                instance = $.data(this, 'JQuery_' + SlidingApps.JQuery.PlugIn.CircularMenu.CircularMenuPlugIn.PLUG_IN_NAME);
            }

            if (instance[action]) {
                instance[action](options);
            } else {
                throw new Error(SlidingApps.JQuery.PlugIn.CircularMenu.CircularMenuPlugIn.PLUG_IN_NAME.toUpperCase() + ' does not support action \'' + action.toUpperCase() + '\'');
            }
        });

    };
})(jQuery, window, document, undefined);