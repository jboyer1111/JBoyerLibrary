<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>
  <xsl:template match="/">
    <Document>
      <xsl:apply-templates select="Document/Header"/>
      <xsl:apply-templates select="Document/Record/Acknowledgement"/>
    </Document>
  </xsl:template>

  <xsl:template match="Document/Header">
    <Header>
      <PacketNum>
        <xsl:value-of select="PacketNum"/>
      </PacketNum>
    </Header>
  </xsl:template>

  <xsl:template match="Document/Record/Acknowledgement">
    <Acknowledgement>
      <FileNumber>
        <xsl:value-of select="FileNumber"/>
      </FileNumber>
      <FileDate>
        <xsl:value-of select="FileDate"/>
        <xsl:value-of select="FileTime"/>
      </FileDate>
      <FeeAmount>
        <xsl:value-of select="FeeAmount"/>
      </FeeAmount>
      <AdditionalFees>
        <xsl:value-of select="AdditionalFees"/>
      </AdditionalFees>
      <FilingOffice>
        <xsl:value-of select="FilingOffice"/>
      </FilingOffice>
      <FileStatus>
        <xsl:choose>
          <xsl:when test="FileStatus/@Status = 'Accepted'">
            1
          </xsl:when>
          <xsl:when test="FileStatus/@Status = 'Rejected'">
            0
          </xsl:when>
          <xsl:otherwise>
            -1
          </xsl:otherwise>
        </xsl:choose>
      </FileStatus>
      <FilingError>
        <xsl:value-of select="Errors/ErrorText"/>
      </FilingError>
      <PDFAck>
      </PDFAck>
    </Acknowledgement>
  </xsl:template>
</xsl:stylesheet>
