import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { PasswordReset } from "./passwordReset";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
}));

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test account group component: passwordReset", () => {
    const testContent = {
        language: "eng",
        caption: "Reset Password",
        button: "Reset",
        labelEmail: "Email address",
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent,
        });

        useDispatchMock.mockReturnValue(jest.fn());
    });

    it("should renders correctly '<PasswordReset />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <PasswordReset />
            </BrowserRouter>
        );
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(html).toMatchSnapshot();
    });
});
