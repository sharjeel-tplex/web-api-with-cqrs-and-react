import { Departments } from "./components/Departments";
import { Employee } from "./components/Employee";
import { CreateEmployee } from "./components/CreateEmployee";
import { UpdateEmployee } from "./components/UpdateEmployee";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/departments',
        element: <Departments />
    },
    {
        path: '/employee',
        element: <Employee />
    },
    {
        path: '/createEmployee',
        element: <CreateEmployee />
    },
    {
        path: '/updateEmployee/:id',
        element: <UpdateEmployee />
    },
];

export default AppRoutes;
