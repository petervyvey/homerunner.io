<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">

  <xsl:template match="node()">
    <xsl:text>{ "</xsl:text>
    <xsl:value-of select="name()"/>
    <xsl:text>" : </xsl:text>
    <xsl:if test="count(node()) = 0">
      <xsl:text>null</xsl:text>
    </xsl:if>
    <xsl:text> }</xsl:text>
  </xsl:template>

</xsl:stylesheet>
