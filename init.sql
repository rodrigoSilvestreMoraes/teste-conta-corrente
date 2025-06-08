CREATE TABLE IF NOT EXISTS account (
    id BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    document VARCHAR(50) NOT NULL,
    status INT NOT NULL,
    openingDate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    updateDate TIMESTAMP WITHOUT TIME ZONE null,
    userName VARCHAR(50) null
);

CREATE INDEX IF NOT EXISTS idx_accountName ON account(name);
CREATE UNIQUE INDEX IF NOT EXISTS uq_accountDocument ON account(document);

CREATE TABLE IF NOT EXISTS balance (
    accountId BIGINT PRIMARY KEY,
    currentBalance NUMERIC(18, 2) NOT NULL,
    updateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    CONSTRAINT fk_balance_account FOREIGN KEY (accountId) REFERENCES account(id)
);

CREATE TABLE IF NOT EXISTS bank_transfer (
    id UUID NOT NULL,
    accountId BIGINT NOT NULL,
    releaseDate TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    operation INT NOT NULL,
    value NUMERIC(18, 2) NOT NULL,
    CONSTRAINT pk_bank_transfer PRIMARY KEY (id, accountId),
    CONSTRAINT fk_bank_transfer_account FOREIGN KEY (accountId) REFERENCES account(id)
);