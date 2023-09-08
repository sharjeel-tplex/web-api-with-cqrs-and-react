import React, { Component, useEffect, useState } from 'react';
import { EmployeesClient, DepartmentsClient } from '../web-api-client.ts';
import './CreateEmployee.css';
import { useNavigate } from 'react-router-dom';

export const CreateEmployee = () => {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        name: '',
        email: '',
        departmentId: '',
        dateOfBirth: '',
        isLoading: false,
    });

    const [departments, setDepartments] = useState([]); // State to store department options

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setFormData({ ...formData, [name]: value });
    };

    useEffect(() => {
        // Fetch department options when the component mounts
        const fetchDepartments = async () => {
            try {
                const client = new DepartmentsClient();
                const departmentsData = await client.getDepartments();
                setDepartments(departmentsData.list);
            } catch (error) {
                console.error('Error fetching departments:', error);
            }
        };

        fetchDepartments();
    }, []); // Empty dependency array to fetch departments once on component mount

    const handleSubmit = async (event) => {
        event.preventDefault();

        const { name, email, departmentId, dateOfBirth } = formData;

        const newEmployee = {
            name,
            email,
            departmentId,
            dateOfBirth,
        };

        try {
            const client = new EmployeesClient();
            const employeeId = await client.createEmployee(newEmployee);

            navigate('/employee'); // Use navigate for redirection

            console.log(`Employee created with ID: ${employeeId}`);
        } catch (error) {
            console.error('Error creating employee:', error);
        }
    };

    return (
        <div className="create-employee-container">
            <h1>Create Employee</h1>
            <form onSubmit={handleSubmit} className="create-employee-form">
                <div>
                    <label>Name:</label>
                    <input 
                        type="text"
                        name="name"
                        value={formData.name}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label>Email:</label>
                    <input
                        type="email"
                        name="email"
                        value={formData.email}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label>Department:</label>
                    <select
                        class="selectDep"
                        name="departmentId"
                        value={formData.departmentId}
                        onChange={handleInputChange}
                        required
                    >
                        <option value="">Select Department</option>
                        {departments.map((department) => (
                            <option key={department.id} value={department.id}>
                                {department.name}
                            </option>
                        ))}
                    </select>
                </div>
                <div>
                    <label>Date of Birth:</label>
                    <input
                        type="date"
                        name="dateOfBirth"
                        value={formData.dateOfBirth}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <button type="submit">Create</button>
                </div>
            </form>
        </div>
    );
};
