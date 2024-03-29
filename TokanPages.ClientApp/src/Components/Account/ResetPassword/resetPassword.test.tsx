import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { shallow } from "enzyme";
import { ResetPassword } from "./resetPassword";
import { ApplicationDefault } from "../../../Store/Configuration";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
}));

describe("test account group component: resetPassword", () => {
    const testContent = {
        language: "eng",
        caption: "Reset Password",
        button: "Reset",
        labelEmail: "Email address",
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(
        <div>
            <ResetPassword content={testContent} isLoading={false} />
        </div>
    );

    beforeEach(() => {
        useSelectorMock.mockClear();
        useDispatchMock.mockClear();
        wrapper.find("ResetPassword").dive();
    });

    it("should renders correctly '<ResetPassword />' when content is loaded.", () => {
        useDispatchMock.mockReturnValue(jest.fn());
        useSelectorMock.mockReturnValue(ApplicationDefault);

        expect(useDispatchMock).toBeCalledTimes(1);
        expect(wrapper).toMatchSnapshot();
    });
});
