<?xml version="1.0"?>

<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:tux="http://myGame/tux">
    <xsl:output method="html"/>
    <xsl:template match="/">
        <html>
            <body>
                <ul>
                    <xsl:apply-templates select="tux:dictionnaire/tux:mot">
                        <xsl:sort select="@niveau" />
                        <xsl:sort select="text()" />
                    </xsl:apply-templates>
                </ul>
            </body>
        </html>
    </xsl:template>
    <xsl:template match="tux:mot">
        <li>Niveau: <xsl:value-of select="@niveau"/></li>
        <li>Text: <xsl:value-of select="text()"/></li>
    </xsl:template>
</xsl:stylesheet>