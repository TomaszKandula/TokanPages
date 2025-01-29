import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import * as Dom from "react-router-dom";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { PasswordUpdate } from "./passwordUpdate";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
}));

jest.mock("react-router-dom", () => ({
    ...(jest.requireActual("react-router-dom") as typeof Dom),
    useLocation: () => ({
        search: "localhost:3000/password-update/?id=dba4043c-7428-4f72-ba13-fe782c7a88fa",
    }),
}));

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test account group component: passwordUpdate", () => {
    const testContent = {
        language: "eng",
        caption: "Update Password",
        button: "Submit",
        labelNewPassword: "New password",
        labelVerifyPassword: "Verify new password",
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent,
        });

        useDispatchMock.mockReturnValue(jest.fn());
    });

    it("should render correctly '<PasswordUpdate />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <PasswordUpdate />
            </BrowserRouter>
        );
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(html).toMatchSnapshot();
    });
});
