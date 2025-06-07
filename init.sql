-- Tabela account
CREATE TABLE IF NOT EXISTS account (
    id BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    document VARCHAR(50) NOT NULL,
    status INT NOT NULL,
    openingDate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    updateDate TIMESTAMP WITHOUT TIME ZONE null,
    userName VARCHAR(50) null
);
-- Índice para busca por nome (não exclusivo)
CREATE INDEX IF NOT EXISTS idx_accountName ON account(name);
-- Índice único para garantir que não existam documentos duplicados
CREATE UNIQUE INDEX IF NOT EXISTS uq_accountDocument ON account(document);

-- Tabela balance
CREATE TABLE IF NOT EXISTS balance (
    accountId BIGINT PRIMARY KEY,
    currentBalance NUMERIC(18, 2) NOT NULL,
    updateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    CONSTRAINT fk_balance_account FOREIGN KEY (accountId) REFERENCES account(id)
);

-- Tabela bank_transfer
CREATE TABLE IF NOT EXISTS bank_transfer (
    id UUID PRIMARY KEY,
    accountId BIGINT NOT NULL,
    releaseDate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    operation INT NOT NULL,
    value NUMERIC(18, 2) NOT NULL,
    CONSTRAINT fk_bank_transfer_account FOREIGN KEY (accountId) REFERENCES account(id)
);

-- Tabela bank_transfer_p2p
CREATE TABLE IF NOT EXISTS bank_transfer_p2p (
    transferId UUID PRIMARY KEY,
    sourceAccount BIGINT NOT NULL,
    destinationAccount BIGINT NOT NULL,
    CONSTRAINT fk_bank_transfer_p2p_transfer FOREIGN KEY (transferId) REFERENCES bank_transfer(id),
    CONSTRAINT fk_bank_transfer_p2p_source_account FOREIGN KEY (sourceAccount) REFERENCES account(id),
    CONSTRAINT fk_bank_transfer_p2p_destination_account FOREIGN KEY (destinationAccount) REFERENCES account(id)
);
