import React, { useState, useEffect } from 'react';
import { EmployeesClient, DepartmentsClient } from '../web-api-client.ts';
import './UpdateEmployee.css';
import { useNavigate, useParams } from 'react-router-dom';

export const UpdateEmployee = () => {
    const navigate = useNavigate();
    const { id } = useParams();
    const [employeeData, setEmployeeData] = useState({
        id,
        name: '',
        email: '',
        departmentId: '',
        dateOfBirth: '',
        isLoading: true,
    });

    const [departments, setDepartments] = useState([]); // State to store department options

    const { name, email, departmentId, dateOfBirth, isLoading } = employeeData;

    useEffect(() => {
        async function fetchEmployeeData() {
            try {
                let client = new EmployeesClient();
                const employee = await client.getEmployeeById(id);

                const formattedDateOfBirth = employee.dateOfBirth
                    ? new Date(employee.dateOfBirth).toISOString().split('T')[0]
                    : '';

                setEmployeeData({
                    ...employeeData,
                    name: employee.name,
                    email: employee.email,
                    departmentId: employee.departmentId,
                    dateOfBirth: formattedDateOfBirth,
                    isLoading: false,
                });
            } catch (error) {
                console.error('Error fetching employee data:', error);
            }
        }

        async function fetchDepartments() {
            try {
                const client = new DepartmentsClient();
                const departmentsData = await client.getDepartments();
                setDepartments(departmentsData.list);
            } catch (error) {
                console.error('Error fetching departments:', error);
            }
        }

        fetchEmployeeData();
        fetchDepartments(); // Fetch department options when the component mounts
    }, [id]);

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setEmployeeData({ ...employeeData, [name]: value });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        const updatedEmployee = {
            id,
            name,
            email,
            departmentId,
            dateOfBirth,
        };

        try {
            let client = new EmployeesClient();
            await client.updateEmployee(id, updatedEmployee);

            navigate('/employee');

            console.log(`Employee updated with ID: ${id}`);
        } catch (error) {
            console.error('Error updating employee:', error);
        }
    };

    if (isLoading) {
        return <p>Loading...</p>;
    }

    return (
        <div className="update-employee-container">
            <h1>Update Employee</h1>
            <form onSubmit={handleSubmit} className="update-employee-form">
                <div>
                    <label>Name:</label>
                    <input
                        type="text"
                        name="name"
                        value={name}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label>Email:</label>
                    <input
                        type="email"
                        name="email"
                        value={email}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label>Department:</label>
                    <select
                        class="selectDep"
                        name="departmentId"
                        value={departmentId}
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
                        value={dateOfBirth}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <button type="submit">Update</button>
                </div>
            </form>
        </div>
    );
};
