-- Tabela account
CREATE TABLE IF NOT EXISTS account (
    id BIGSERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    document VARCHAR(50) NOT NULL,
    status_account INT NOT NULL,
    opening_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    update_date TIMESTAMP WITHOUT TIME ZONE NULL
);

-- Índice para busca por nome (não exclusivo)
CREATE INDEX IF NOT EXISTS idx_account_name ON account(name);
-- Índice único para garantir que não existam documentos duplicados
CREATE UNIQUE INDEX IF NOT EXISTS uq_account_document ON account(document);

-- Tabela balance
CREATE TABLE IF NOT EXISTS balance (
    account_id BIGINT PRIMARY KEY,
    current_balance NUMERIC(18, 2) NOT NULL,
    update_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    CONSTRAINT fk_balance_account FOREIGN KEY (account_id) REFERENCES account(id)
);

-- Tabela bank_transfer
CREATE TABLE IF NOT EXISTS bank_transfer (
    id UUID PRIMARY KEY,
    account_id BIGINT NOT NULL,
    release_date TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    operation INT NOT NULL,
    value NUMERIC(18, 2) NOT NULL,
    CONSTRAINT fk_bank_transfer_account FOREIGN KEY (account_id) REFERENCES account(id)
);

-- Tabela bank_transfer_p2p
CREATE TABLE IF NOT EXISTS bank_transfer_p2p (
    transfer_id UUID PRIMARY KEY,
    source_account BIGINT NOT NULL,
    destination_account BIGINT NOT NULL,
    CONSTRAINT fk_bank_transfer_p2p_transfer FOREIGN KEY (transfer_id) REFERENCES bank_transfer(id),
    CONSTRAINT fk_bank_transfer_p2p_source_account FOREIGN KEY (source_account) REFERENCES account(id),
    CONSTRAINT fk_bank_transfer_p2p_destination_account FOREIGN KEY (destination_account) REFERENCES account(id)
);
