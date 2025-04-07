<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="html" encoding="UTF-8" />

	<xsl:template match="/">
		<html>
			<head>
				<title>История конверсий</title>
				<style>
					table {
					border-collapse: collapse;
					width: 80%;
					margin: 20px auto;
					}
					th, td {
					border: 1px solid #aaa;
					padding: 8px;
					text-align: center;
					}
					th {
					background-color: #f2f2f2;
					}
					body {
					font-family: sans-serif;
					}
				</style>
			</head>
			<body>
				<h2 style="text-align:center;">История конверсий</h2>
				<table>
					<tr>
						<th>Тип</th>
						<th>Из</th>
						<th>В</th>
						<th>Значение</th>
						<th>Результат</th>
						<th>Дата</th>
					</tr>
					<xsl:for-each select="ArrayOfConversionHistory/ConversionHistory">
						<tr>
							<td>
								<xsl:value-of select="Type" />
							</td>
							<td>
								<xsl:value-of select="From" />
							</td>
							<td>
								<xsl:value-of select="To" />
							</td>
							<td>
								<xsl:value-of select="Value" />
							</td>
							<td>
								<xsl:value-of select="Result" />
							</td>
							<td>
								<xsl:value-of select="Date" />
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
