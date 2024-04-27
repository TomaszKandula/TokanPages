import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { BrowserRouter } from "react-router-dom";
import { render } from "enzyme";
import { UserSignin } from "./userSignin";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
    useHistory: () => jest.fn(),
}));

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn(),
}));

describe("test account group component: userSignin", () => {
    const testContent = {
        language: "eng",
        caption: "Sign in",
        button: "Sign in",
        link1: "Don't have an account?",
        link2: "Forgot password?",
        labelEmail: "Email address",
        labelPassword: "Password",
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent,
        });

        useDispatchMock.mockReturnValue(jest.fn());
    });

    it("should render correctly '<UserSignin />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <UserSignin />
            </BrowserRouter>
        );
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(html).toMatchSnapshot();
    });
});
