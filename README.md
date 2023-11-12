# AtmosClearTestTask

1. repository is available in https://github.com/KOrenits/AtmosClearTestTask
2. after repository cloning go to TestTask folder and open cmd
3. in cmd write in  dotnet run
4. after building  find the line where it says "Now listening on: http://localhost:5191"
    4.1 keep in mind that there can be another port number.
    4.2 copy this line in web browser  and also add /swagger/index.html
    4.3 completed link should look like this -  http://localhost:yourPortNumberfromCmd/swagger/index.html
5. After that swagger will open and there will be 5 http methods. To test any of methods simply:
    5.1 press on one of them. 
    5.2 then in upper right corner press "try it out".
    5.3 If method has input parameters they will be presented as white blocks.
    For example method "createTask" will have 2 input fields title and description.
    5.4 After input is typed in press blue button "execute"
    5.5 scroll down till you see section "Response body" there you can see response data depending on method
    it can be different kind of information - task list, simple task, error message.

6. About methods and validation. If some criteria of validation is not fulfilled in response body will be error message.
    6.1 createTask - adds new created Task to system. User inputs title and description values.
        Validations:
            a. Title and description fields cannot be empty.
            b. can not create another task with the same name.
    6.2 getAllTasks - returns all created tasks.
        Validations:
            a. Search if there is any tasks created
    6.3 getTaskById - returns one task by id. User inputs task Id value.
        Validations:
            a. Search if there is any tasks created.
            b. Search if there is task with user specified Id
    6.4 updateTaskById - updates one task by Id. User inputs Id value which task needs to be updated and also enters new title and description values if necessary.
        Validations:
            a. Search if there is any tasks created
            b. Search if there is task with user specified Id
            c. Title and description fields cannot be empty.
            d. can not update task with the same name as another task.
    6.5 deleteTaskById - deletes task by Id. User inputs task Id which needs to be deleted
        Validations:
            a. Search if there is any tasks created
            b. Search if there is task with user specified Id

