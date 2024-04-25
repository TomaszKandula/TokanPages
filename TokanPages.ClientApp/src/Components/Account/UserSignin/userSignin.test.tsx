import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { shallow } from "enzyme";
import { UserSignin } from "./userSignin";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
    useHistory: () => jest.fn(),
}));

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn()
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
    const wrapper = shallow(
        <div>
            <UserSignin />
        </div>
    );

    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent
        });

        wrapper.find("UserSignin").dive();
    });

    it("should render correctly '<UserSignin />' when content is loaded.", () => {
        useDispatchMock.mockReturnValue(jest.fn());
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(wrapper).toMatchSnapshot();
    });
});
