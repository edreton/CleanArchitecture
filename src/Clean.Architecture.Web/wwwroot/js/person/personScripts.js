$(document).ready(function () {
    var apiUrl = $('#apiUrl').val();
    App.Person.apiUrl = apiUrl + "/Persons";
    console.log(App.Person.datagrid);
    App.Person.loadPersons();
});

App.Person = {
    personData: {
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber: '',
        age: 0
    },
    datagrid: $('#datagrid-persons'),
    controlApi: '',
    apiUrl: '',
    loadPersons: function () {
        $.ajax({
            method: "GET",
            url: App.Person.apiUrl,
            dataType: 'json',
            success: function (data) {
                //console.log(data);
                var mappedPersons = data.persons.map(function (person) {
                    return {
                        Id: person.id,
                        FirstName: person.firstName,
                        LastName: person.lastName,
                        Email: person.email,
                        PhoneNumber: person.phoneNumber,
                        Age: person.age
                    };
                });
                App.Person.datagrid[0].ej2_instances[0].dataSource = mappedPersons;
            },
            error: function (error) {
                console.log(error);
            }
        });
    },
    actionComplete: function (e) {
        var id = 0;
        if (e.requestType === "beginEdit") {
            id = e.primaryKeyValue[0];
        }
        if (e.requestType === "save" && e.action === "add") {
            id = 0;
            App.Person.personData.id = id;
            App.Person.personData.firstName = e.data.FirstName;
            App.Person.personData.lastName = e.data.LastName;
            App.Person.personData.email = e.data.Email;
            App.Person.personData.phoneNumber = e.data.PhoneNumber;
            App.Person.personData.age = e.data.Age;
            $.ajax({
                method: "POST",
                data: JSON.stringify(App.Person.personData),
                url: App.Person.apiUrl,
                contentType: "application/json",
                dataType: 'json',
                success: function (data) {
                    App.Person.personData.id = data.id;
                    App.Person.datagrid[0].ej2_instances[0].componentRefresh();
                    App.Person.loadPersons();
                    App.Person.datagrid[0].ej2_instances[0].refresh();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        if (e.requestType === "delete") {
            id = e.data[0].Id;
            $.ajax({
                method: "DELETE",
                url: App.Person.apiUrl + "/" + id,
                success: function (data) {
                    //console.log(data);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        if (e.requestType === "save" && e.action === "edit") {
            id = e.primaryKeyValue[0];
            App.Person.personData.id = id;
            App.Person.personData.firstName = e.data.FirstName;
            App.Person.personData.lastName = e.data.LastName;
            App.Person.personData.email = e.data.Email;
            App.Person.personData.phoneNumber = e.data.PhoneNumber;
            App.Person.personData.age = e.data.Age;
            $.ajax({
                method: "PUT",
                data: JSON.stringify(App.Person.personData),
                url: App.Person.apiUrl,
                contentType: "application/json",
                dataType: 'json',
                success: function (data) {
                    //console.log(data);
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    },

}