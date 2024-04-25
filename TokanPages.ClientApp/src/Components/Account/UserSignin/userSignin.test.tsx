import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { shallow } from "enzyme";
import { UserSignin } from "./userSignin";
import { ApplicationDefault } from "../../../Store/Configuration";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
    useHistory: () => jest.fn(),
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

    let data = ApplicationDefault;
    data.contentUserSignin.isLoading = false;
    data.contentUserSignin.content = testContent;

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(
        <div>
            <UserSignin />
        </div>
    );

    beforeEach(() => {
        useSelectorMock.mockClear();
        useDispatchMock.mockClear();
        wrapper.find("UserSignin").dive();
    });

    it("should render correctly '<UserSignin />' when content is loaded.", () => {
        useDispatchMock.mockReturnValue(jest.fn());
        useSelectorMock.mockReturnValue(data);

        expect(useDispatchMock).toBeCalledTimes(1);
        expect(wrapper).toMatchSnapshot();
    });
});
