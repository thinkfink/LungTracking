CREATE PROCEDURE [dbo].[spCalcMAP]
	@BPsystolic int, 
	@BPdiastolic int
AS
	SELECT ((@BPsystolic + (2 * @BPdiastolic)) / 3) AS MAP
RETURN 0
