

    function CustomerViewModel() {
        var self = this;

        self.Departmentval = ko.observableArray(['IT', 'HR', 'SALES', 'FINANCE']);
           self.Genders = ko.observableArray(['Male', 'Female']);
           self.Students = ko.observableArray([]);
            self.Id = ko.observable();
        self.Name = ko.observable();
        self.City = ko.observable();
        self.Address = ko.observable();
        self.Department = ko.observable();
        self.Gender = ko.observable();
            self.Date= ko.observable();
  
        Students = ko.observableArray([]);
        var Customer = {
            Id: self.Id,
            Name: self.Name,
            City: self.City,
            Address: self.Address,
            Department: self.Department,
            Gender: self.Gender,
            self:self.Date
        };
        self.Customer = ko.observable();
        self.Customers = ko.observableArray();
 
        self.create = function () {
            //if (self.Name() == undefined || self.Name().length<2) {
            //    alert("Name can't be blank");
            //    return false;
            //}
            //else if (self.City() == undefined || self.City().length<2) {
            //    alert("Please enter product");
            //    return false;
            //}
            //else if (self.Address() == undefined) {
            //    alert("Please select 'Address'")
            //    return false;
            //}
           
            //if (Customer.Name() != "" &&
            //    Customer.City() != "" && Customer.Address() != "" && Customer.Department() != "" && Customer.Date() != "") {
                $.ajax({
                    url: '/Studentinfoes/Create',
                    cache: false,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: ko.toJSON(Customer),
                    success: function (data) {
                        self.Customers.push(data);
                        self.Name("");
                        self.City("");
                        self.Address("");
                        self.Department("");
                        self.Date("");
                        self.Gender("");

                        alert('Student Added Successfully ..!')
                    }
                }).fail(
                function (xhr, textStatus, err) {
                    alert(err);
                });
            //}
            //else {
            //    alert('Please Enter All the Values !!');
            //}
        }

        self.delete = function (Customer) {
            if (confirm('Are you sure to Delete "' + Customer.Name + '" Details ??')) {
                var id = Customer.Id;
                $.ajax({
                    url: 'Customer/DeleteCustomer/' + id,
                    cache: false,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: ko.toJSON(id),
                    success: function (data) {
                        self.Customer.remove(Customers); alert("Customer Deleted successfully ..!");
                    }
                }).fail(
                function (xhr, textStatus, err) {
                    self(err);
                });
            }
        }

        self.edit = function (Customer) {
            //self.Customer(Customer);
            document.getElementById('name_').value=Customer.Name;

        }   
        self.update = function () {
            var Customer = self.Customer();
            $.ajax({
                url: 'Customer/EditCustomer',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(Customer),
                success: function (data) {
                    self.Customers.removeAll();

                    self.Customers(data);
                    self.Customer(null);
                    alert("Record Updated Successfully");
                }
            })
            .fail(
            function (xhr, textStatus, err) {
                alert("could not update ");
            });
        }

        self.reset = function () {
            self.Name("");
            self.product("");
            self.Address("");
            self.EmailId("");
            self.MobileNumber("");
        }


        self.cancel = function () {
            self.Customer(null);
        }

        $.ajax({
            url: '/Studentinfoes/Index',
            cache: false,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            data: {},
            success: function (data) {
                //ko.mapping.fromJSON(data,CustomerViewModel);
                self.Customers(data);
            }
        });
    }

    var viewModel = new CustomerViewModel();
    ko.applyBindings(viewModel);


