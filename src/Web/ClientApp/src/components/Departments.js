import React, { Component } from 'react';
import followIfLoginRedirect from './api-authorization/followIfLoginRedirect';
import { DepartmentsClient } from '../web-api-client.ts';

export class Departments extends Component {
    static displayName = "Departments";

    constructor(props) {
        super(props);
        this.state = { departments: [], loading: true };
    }

    componentDidMount() {
        debugger;
        this.populateDepartmentsData();
    }

    static renderDepartmentsTable(departments) {
        return (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Created</th>
                        <th>Last Modified</th>
                    </tr>
                </thead>
                <tbody>
                    {departments.map(department =>
                        <tr key={department.id}>
                            <td>{department.id}</td>
                            <td>{department.name}</td>
                            <td>{new Date(department.created).toLocaleDateString()}</td>
                            <td>{new Date(department.lastModified).toLocaleDateString()}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
       
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Departments.renderDepartmentsTable(this.state.departments);

        return (
            <div>
                <h1 id="tableLabel">Departments</h1>
                <p>Departments in the company.</p>
                {contents}
            </div>
        );
    }

    async populateDepartmentsData() {
        let client = new DepartmentsClient();
        const data = await client.getDepartments();
        this.setState({ departments: data.list, loading: false });
    }

    //async populateWeatherDataOld() {
    //  const response = await fetch('weatherforecast');
    //  followIfLoginRedirect(response);
    //  const data = await response.json();
    //  this.setState({ forecasts: data, loading: false });
    //}
}
