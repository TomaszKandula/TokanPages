import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { BrowserRouter as Router } from "react-router-dom";
import { render } from "@testing-library/react";
import { UserSignup } from "./userSignup";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test account group component: userSignup", () => {
    const testContent = {
        language: "",
        caption: "",
        button: "",
        link: "",
        warning: "",
        consent: "",
        labelFirstName: "",
        labelLastName: "",
        labelEmail: "",
        labelPassword: "",
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent,
        });

        useDispatchMock.mockReturnValue(jest.fn());
    });

    it("should render correctly '<UserSignup />' when content is loaded.", () => {
        const html = render(
            <Router>
                <UserSignup />
            </Router>
        );
        expect(useDispatchMock).toBeCalledTimes(2);
        expect(html).toMatchSnapshot();
    });
});
