SELECT * FROM users
SELECT * FROM transfers
SELECT * FROM transfer_types
SELECT * FROM transfer_statuses
SELECT u.username user_to, t.* FROM users u
	JOIN accounts a ON u.user_id = a.user_id
	JOIn transfers t ON a.account_id = t.account_to
	WHERE t.account_to IN (
SELECT account_to FROM transfers t
	JOIN accounts a ON t.account_from = a.account_id
	JOIN users u ON a.user_id = u.user_id
	WHERE u.username = 'jasonsimon' AND t.transfer_status_id != 1)

BEGIN TRANSACTION
UPDATE accounts SET balance = balance - @amount
WHERE account_id = @accountFrom UPDATE accounts SET balance = balance + @amount
WHERE account_id = @accountTo

INSERT transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount)
VALUES (@type, @status, @accountFrom, @accountTo, @amount) SELECT @@IDENTITY
COMMIT TRANSACTION

SELECT * FROM users u
	JOIN accounts a ON u.user_id = a.user_id
	WHERE a.account_id = @accountId
UPDATE accounts SET balance = balance - @amount
	WHERE account_id = @accountFrom
UPDATE accounts SET balance = balance + @amount
	WHERE account_id = @accountTo


SELECT * FROM accounts
SELECT * FROM accounts 
	JOIN users ON accounts.user_id = users.user_id WHERE users.username = 'amycave'

SELECT user_id, username, password_hash, salt FROM users WHERE username = @username
SELECT account_id FROM accounts a
	JOIN users u ON a.user_id = u.user_id
	WHERE u.username = @username

SELECT u.username user_to, t.* FROM users u JOIN accounts a ON u.user_id = a.user_id
JOIN transfers t ON a.account_id = t.account_to WHERE t.account_to IN(SELECT account_to FROM transfers t
JOIN accounts a ON t.account_from = a.account_id JOIN users u ON a.user_id = u.user_id
WHERE u.username = 'jasonsimon' AND t.transfer_status_id != 1)

SELECT * From transfers t WHERE account_from = 1 OR account_to = 1

SELECT * FROM transfers t
JOIN accounts a ON t.account_from = a.account_id JOIN users u ON a.user_id = u.user_id
WHERE u.username = 'jasonsimon' AND t.transfer_status_id != 1

SELECT u.user_id, u.username, a.account_id, t.transfer_id, t.transfer_type_id, t.transfer_status_id, account_to, amount
INTO #temp
FROM users u
	JOIN accounts a ON u.user_id = a.user_id
	JOIN transfers t ON a.account_id = t.account_from
	WHERE username = 'jasonsimon'
	
	
	AND account_to IN (SELECT account_to FROM users u
	JOIN accounts a ON u.user_id = a.user_id
	JOIN transfers t ON a.account_id = t.account_to
	WHERE username = 'amycave')

SELECT u.username user_to, t.* FROM users u JOIN accounts a ON u.user_id = a.user_id " +
                        "JOIN transfers t ON a.account_id = t.account_to WHERE t.account_to IN(SELECT account_to FROM transfers t " +
                        "JOIN accounts a ON t.account_from = a.account_id JOIN users u ON a.user_id = u.user_id " +
                        "WHERE u.username = @userName AND t.transfer_status_id != 1)

Declare @userName varchar(40) = 'amycave'

SELECT uTO.username user_to, t.*, uFROM.username user_from
FROM transfers t
JOIN accounts aTO ON aTO.account_id = t.account_to
JOIN users uTO ON uTO.user_id = aTO.user_id
JOIN accounts aFROM ON aFROM.account_id = t.account_from
JOIN users uFROM ON uFROM.user_id = aFROM.user_id
WHERE @userName IN (uFROM.username)