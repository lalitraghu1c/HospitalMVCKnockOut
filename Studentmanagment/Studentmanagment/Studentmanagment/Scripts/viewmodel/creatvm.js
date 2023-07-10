var urlPath = window.location.pathname;

$(function () {
    ko.applyBindings(CreateVM);
    ko.validation.init({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null,
        errorElementClass: 'errorStyle',
        errorClass: 'errorStyle',
        decorateElementOnModified: true,
        decorateInputElement: true
    }, true);
    CreateVM.getStudents();

});

//$(document).ready(function() {  
  
//    ko.validation.init({  
  
//        registerExtenders: true,  
//        messagesOnModified: true,  
//        insertMessages: true,  
//        parseInputAttributes: true,  
//        errorClass: 'errorStyle',  
//        messageTemplate: null  
  
//    }, true);  

var CreateVM = {

   
    Departmentval:['IT', 'HR', 'SALES', 'FINANCE'],
    chosendepartment: ko.observableArray([]),
    Genders: ko.observableArray(['Male', 'Female']),
    Students: ko.observableArray([]),
    Id: ko.observable(),
    Name: ko.observable().extend({ required: { message: "please Enter your Name" }, minLength: 2, maxLength: 255 }),
    City: ko.observable().extend({ required: { message: "please Enter your City" }, minLength: 2, maxLength: 255 }),
    Address: ko.observable().extend({ required: { message: "please Enter your Address" }, minLength: 2, maxLength: 255 }),
    Department: ko.observable().extend({
        required: { message: "You must select a Department" }
    }),
    Gender: ko.observable().extend({required: { message: "You must select a Gender" }}),
    Date: ko.observable().extend({
        required: { message: "You must select a Date" }}),
  
    Students: ko.observableArray([]),
    CreateVM:ko.observable(),
    errors: ko.validation.group(this, { deep: true, observable: false }),
    getStudents: function () {
        var add = document.getElementById('create');
        add.type = "button";
        var edt = document.getElementById('edit')
        edt.type = "hidden";


        var self = this;
        $.ajax({
            type: "get",
            url: '/Studentinfoes/Index',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                self.Students(data); //Put the response in ObservableArray  
            },
            error: function (err) {
                alert(err.status + " : " + err.statusText);
            }
        });
    },


    SaveStudent: function () {
        if (CreateVM.errors().length === 0) {
            $.ajax({
                url: '/Studentinfoes/Create',
                type: 'post',
                dataType: 'json',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {
                },
                error: function (err) {
                    if (err.responseText == "Creation Failed")
                    { window.location.href = '/Studentinfoes/Create/'; }
                    else {
                        alert("Status:" + err.responseText);
                        window.location.href = '/Studentinfoes/Create/';;
                    }
                },
                complete: function () {
                    window.location.href = '/Studentinfoes/Create/';
                }
            });
        }
        else {
            alert('Please check your submission.');
            self.errors.showAllMessages();
        }
       
    },


    saveedit:function(){
        $.ajax({
            url: '/Studentinfoes/Edit',
            type: 'post',
            dataType: 'json',
            data: ko.toJSON(this),
            contentType: 'application/json',
            success: function (result) {
            },
            error: function (err) {
                if (err.responseText == "Creation Failed")
                { window.location.href = '/Studentinfoes/Create/'; }
                else {
                    alert("Status:" + err.responseText);
                    window.location.href = '/Studentinfoes/Create/';;
                }
            },
            complete: function () {
                window.location.href = '/Studentinfoes/Create/';
            }
        });
    },

 
    exportpdf: function () {
        $.ajax({
            url: '/Studentinfoes/Export',
            type: 'get',
            dataType: 'json',
            contentType: 'application/json',
            success: function (result) {
            },
            error: function (err) {
                if (err.responseText == "Creation Failed")
                { window.location.href = '/Studentinfoes/Create/'; }
                else {
                    alert("Status:" + err.responseText);
                    window.location.href = '/Studentinfoes/Create/';;
                }
            },
            complete: function () {
                window.location.href = '/Studentinfoes/Create/';
            }
        });
    },
  
	




};



self.editStudent = function (student) {
    //$.ajax({
    //    type: "get",
    //    url: '/Studentinfoes/Edit/' + student.Id,
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (data) {
    //        var self = CreateVM;
    //        self.Id =data.Id;

    //        self.Name = ko.observable(data.Name);
    //        self.City = ko.observable(data.City);
    //        self.Address = ko.observable(data.Address);
    //        //CreateVM.Gender = ko.observable(data.Gender);
    //        //CreateVM.Department = ko.observable(data.Department); //Put the response in ObservableArray  
    //    },
    //    error: function (err) {
    //        alert(err.status + " : " + err.statusText);
    //    }
    //});
    var add = document.getElementById('create');
    add.type = "hidden";
    var edt = document.getElementById('edit')
    edt.type = "button";
   
    
    //document.getElementById('id').value = student.Id;
    CreateVM.Id(student.Id);
    CreateVM.Name(student.Name.trim());
    CreateVM.City(student.City.trim());
    CreateVM.Address(student.Address.trim());
    CreateVM.Department(student.Department.trim())
    //CreateVM.chosendepartment.push(student.Department);
    CreateVM.Date(student.Date.trim());
    CreateVM.Gender(student.Gender.trim());
    //var nm = document.getElementById('name');
    //document.getElementById('city').value = student.City;
    //document.getElementById('address').value = student.Address;
    //document.getElementById('date').value = student.Date;

    //var radiov = student.Gender;
    //if (radiov.trim() == "Male") {
    //    var gender = document.getElementById('male')
    //    gender.checked = true;
    //}

    //if (radiov.trim() == "Female") {
    //    var gender = document.getElementById('female')
    //    gender.checked = true;
    //}


    //var deptv = student.Department;
    //var opt = document.getElementById("dept");
    //for (i = 0; i < opt.length; i++) {
    //    var optval = opt.options[i].innerText;
    //    if (optval == deptv.trim()) {
    //        opt.options[i].selected = true;
    //        return;
    //    }


      

    };

    

   // CreateVM.Id = student.Id,
   // CreateVM.Name =student.Name,
   // CreateVM.City = ko.observable(student.City),
   // CreateVM.Address = ko.observable(student.Address),
   //CreateVM.Department = ko.observable(student.Department),
   //CreateVM.Gender = ko.observable(student.Gender),
   //CreateVM.Date = ko.observable(student.Date)








//function editstudents(data) {
//    self.Id = ko.observable(data.Id);
//    self.Name = ko.observable(data.Name);
//    self.City = ko.observable(data.City);
//    self.Address = ko.observable(data.Address);
//    self.Gender = ko.observable(data.Gender);
//    self.Department = ko.observable(data.Department);
//    //this.Date = ko.observable(data.Date);
//}
self.deleteStudent = function (student) {
    //window.location.href = '/Studentinfoes/Delete/' + student.Id;

    $.ajax({
       url: '/Studentinfoes/Delete/' + student.Id,
        type: 'get',
        dataType: 'json',
        contentType: 'application/json',
        success: function (result) {
        },
        error: function (err) {
            if (err.responseText == "Deletion Failed")
            { window.location.href = '/Studentinfoes/Create/'; }
            else {
                alert("Status:" + err.responseText);
                window.location.href = '/Studentinfoes/Create/';;
            }
        },
        complete: function () {
            window.location.href = '/Studentinfoes/Create/';
        }
    });
};
//Model  
function Students(data) {
    this.Id = ko.observable(data.Id);
    this.Name = ko.observable(data.Name);
    this.City = ko.observable(data.City);
    this.Address = ko.observable(data.Address);
    this.Gender = ko.observable(data.Gender);
    this.Department = ko.observable(data.Department);
    this.Date= ko.observable(data.Date);
}