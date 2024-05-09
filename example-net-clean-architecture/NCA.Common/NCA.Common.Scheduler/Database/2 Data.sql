TRUNCATE TABLE SCHEDULER_JOB_API_REST;

INSERT INTO SCHEDULER_JOB_API_REST(CODE, ENDPOINT, SCHEDULE, STATUS) VALUES
('SystemNotifications', 'http://localhost:5002/api/v1/Tasks/SystemNotifications', '0/5 * * ? * *', 'AC'),
-- 'ContactSynchronization', 'http://localhost:5002/api/v1/Tasks/ContactSynchronization', '0/5 * * ? * *', 'AC'),
('NotificationBuilder', 'http://localhost:5002/api/v1/Tasks/NotificationBuilder', '0/5 * * ? * *', 'AC'),
('CheckNotificationRetry', 'http://localhost:5002/api/v1/Tasks/CheckNotificationRetry', '0/5 * * ? * *', 'AC'),
('NotificationLogMaintenance', 'http://localhost:5002/api/v1/Tasks/NotificationLogMaintenance', '0/5 * * ? * *', 'AC'),
('NotificationLogTransfer', 'http://localhost:5002/api/v1/Tasks/NotificationLogTransfer', '0/5 * * ? * *', 'AC'),
-- ('NotificationSynchronization', 'http://localhost:5002/api/v1/Tasks/NotificationSynchronization', '0/5 * * ? * *', 'AC'),
('TemplateMaintenance', 'http://localhost:5002/api/v1/Tasks/TemplateMaintenance', '0/5 * * ? * *', 'AC'),
('NotificationLogTracking', 'http://localhost:5002/api/v1/Tasks/NotificationLogTracking', '0/5 * * ? * *', 'AC')
;