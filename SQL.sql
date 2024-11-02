CREATE DATABASE ToDoListDB
GO
USE ToDoListDB
GO

CREATE TABLE ToDo (
    Id INT PRIMARY KEY IDENTITY,
    JobToDo NVARCHAR(255),
    Status NVARCHAR(255),
    StartDate DATE
);

SET DATEFORMAT DMY
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Call a friend', N'In Progress', '27/10/2024');
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Read programming book', N'Completed', '28/10/2024');
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Prepare presentation materials', N'Completed', '29/10/2024');
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Morning exercise', N'Completed', '30/10/2024');
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Cook dinner', N'Completed', '31/10/2024');
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Complete math homework', N'Incomplete', '01/11/2024');
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Practice SQL', N'In Progress', '02/11/2024');
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Study English', N'Incomplete', '03/11/2024');
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Clean the room', N'Incomplete', '04/11/2024');
INSERT INTO ToDo (JobToDo, Status, StartDate) VALUES (N'Go outside and Touch Grass', N'Incomplete', '05/11/2024');

Select * From ToDo
