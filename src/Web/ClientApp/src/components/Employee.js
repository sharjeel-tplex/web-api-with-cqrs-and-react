import React, { Component } from 'react';
import { EmployeesClient, DepartmentsClient } from '../web-api-client.ts';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import './Employee.css';

export class Employee extends Component {
    static displayName = "Employee";

    constructor(props) {
        super(props);
        this.state = {
            employees: [],
            loading: true,
            currentPage: 1,
            itemsPerPage: 10,
            totalCount: 0,
            totalPages: 0,
            searchName: "",
            searchEmail: "",
            searchDepartment: "",
            departments: [],
        };
        this.searchEmployees = this.searchEmployees.bind(this);
    }

    async loadDepartments() {
        const client = new DepartmentsClient(); // Replace YourApiClient with the appropriate client
        try {
            debugger
            const departments = await client.getDepartments();
            console.log("Departments:", departments.list); 
            let depData = departments.list
            this.setState({ departments: depData });
        } catch (error) {
            console.error("Error fetching departments:", error);
        }
    }
    

     componentDidMount() {
         this.populateEmployeesData();
         this.loadDepartments();
    }

    nextPage = () => {
        this.setState((prevState) => ({
            currentPage: prevState.currentPage + 1,
        }), () => {
            this.populateEmployeesData();
        });
    };

    prevPage = () => {
        this.setState((prevState) => ({
            currentPage: prevState.currentPage - 1,
        }), () => {
            this.populateEmployeesData();
        });
    };

     async deleteEmployee(employeeId){
         const client = new EmployeesClient();

        try {
            await client.deleteEmployee(employeeId);
            this.populateEmployeesData();
        } catch (error) {
            console.error(`Error deleting employee with ID ${employeeId}:`, error);
        }
    }

     renderEmployeesTable(employees) {
        return (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Departmnt</th>
                        <th>Date Of Birth</th>
                        <th>Created</th>
                        <th>Last Modified</th>
                        <th>Actions</th> 
                    </tr>
                </thead>
                <tbody>
                    {employees.map(employee =>
                        <tr key={employee.id}>
                            <td>{employee.id}</td>
                            <td>{employee.name}</td>
                            <td>{employee.email}</td>
                            <td>{employee.departmentName}</td>
                            <td>{new Date(employee.dateOfBirth).toLocaleDateString()}</td>
                            <td>{new Date(employee.created).toLocaleDateString()}</td>
                            <td>{new Date(employee.lastModified).toLocaleDateString()}</td>
                            <td>
                                <Link to={`/updateEmployee/${employee.id}`}>
                                    <button className="edit-button">
                                        <FontAwesomeIcon icon={faEdit} />
                                    </button>
                                </Link>
                                <button className="delete-button" onClick={() => this.deleteEmployee(employee.id)}>
                                    <FontAwesomeIcon icon={faTrash} />
                                </button>
                            </td>

                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        console.log("currentPage:", this.state.currentPage);
        const { currentPage, itemsPerPage, employees, totalPages, loading } = this.state;
        const displayedEmployees = employees

        let contents = loading ? (
            <p><em>Loading...</em></p>
        ) : (
                <div>
                    <div>
                        {/* ... Render search fields */}
                        <div className="search-fields">
                            <input
                                type="text"
                                placeholder="Search by Name"
                                value={this.state.searchName}
                                onChange={(e) => this.setState({ searchName: e.target.value })}
                            />
                            <input
                                type="text"
                                placeholder="Search by Email"
                                value={this.state.searchEmail}
                                onChange={(e) => this.setState({ searchEmail: e.target.value })}
                            />
                            <select
                                value={this.state.searchDepartment}
                                onChange={(e) => this.setState({ searchDepartment: e.target.value })}
                            >
                                <option value="">All Departments</option>
                                {this.state.departments.map((department) => (
                                    <option key={department.id} value={department.id}>
                                        {department.name}
                                    </option>
                                ))}
                            </select>
                            <button className="search-button" onClick={this.searchEmployees}>Search</button>
                        </div>

                        </div>
                {this.renderEmployeesTable(displayedEmployees)}

            </div>
        );

        return (
            <div className="employee-container">
                <div className="employee-header">
                    <div>
                        <h1 id="tableLabel">Employee</h1>
                        <p>Employees in the company.</p>
                    </div>
                    <div>
                        <Link to="/createEmployee">
                            <button className="create-employee-button">Create Employee</button>
                        </Link>
                    </div>
                </div>
                {contents}
                <div className="pagination">
                    <p class="paginationText">Page {currentPage} of {totalPages}</p>
                    {currentPage > 1 && (
                        <button className="pagination-button" onClick={this.prevPage}>Previous</button>
                    )}
                    {currentPage < totalPages && (
                        <button className="pagination-button" onClick={this.nextPage}>Next</button>
                    )}
                </div>
            </div>
        );
    }

    async populateEmployeesData() {
        debugger;
        const { currentPage, itemsPerPage, searchName, searchEmail, searchDepartment } = this.state;
        let client = new EmployeesClient();
       

        try {
            const data = await client.getEmployeesWithPagination(
                searchName,
                searchEmail,
                searchDepartment ? searchDepartment : null,
                currentPage,
                itemsPerPage
            );
            this.setState({
                employees: data.items,
                totalCount: data.totalCount,
                totalPages: data.totalPages,
                loading: false,
            });
        } catch (error) {
            console.error("Error fetching employees:", error);
            this.setState({ loading: false });
        }
    }

    async searchEmployees() {
        this.setState({ currentPage: 1, loading: true }, () => {
            this.populateEmployeesData();
        });
    }

   
    //async populateEmployeesData() {
    //    const { currentPage, itemsPerPage } = this.state;
    //    let client = new EmployeesClient();

    //    try {
    //        const data = await client.getEmployeesWithPagination("", "", null, currentPage, itemsPerPage);
    //        console.log(data)
    //        this.setState({ employees: data.items, totalCount: data.totalCount, totalPages: data.totalPages, loading: false });
    //    } catch (error) {
    //        console.error("Error fetching employees:", error);
    //        this.setState({ loading: false });
    //    }
    //}

 
}
