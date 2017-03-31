CREATE TABLE [dbo].[FBAccountingTransactionType] (
    [accounting_transaction_type_code] SMALLINT     NOT NULL,
    [accounting_transaction_type_name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_FBAccountingTransactionType] PRIMARY KEY CLUSTERED ([accounting_transaction_type_code] ASC)
);

