// jest-dom adds custom jest matchers for asserting on DOM nodes.
// allows you to do things like:
// expect(element).toHaveTextContent(/react/i)
// learn more: https://github.com/testing-library/jest-dom
import "@testing-library/jest-dom";
import Enzyme from "enzyme";
import Adapter from "enzyme-adapter-react-16";
import enableHooks from "jest-react-hooks-shallow";

enableHooks(jest);

jest.mock("react-redux", () => (
{
    useSelector: jest.fn(),
    useDispatch: jest.fn()
}));

jest.mock("connected-react-router", () => (
{
    connectRouter: jest.fn(),
    routerMiddleware: jest.fn()
}));

Enzyme.configure({ adapter: new Adapter(), disableLifecycleMethods: false });

// environmental variables used during all tests
process.env.REACT_APP_API_VER="1";
process.env.REACT_APP_BACKEND="http://localhost:5000";
