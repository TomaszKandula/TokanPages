import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import * as Dom from "react-router-dom";
import { shallow } from "enzyme";
import { UpdatePassword } from "./updatePassword";
import { ApplicationDefault } from "../../../Store/Configuration";

jest.mock("react-router", () => ({
    ...(jest.requireActual("react-router") as typeof Router),
}));

jest.mock("react-router-dom", () => ({
    ...(jest.requireActual("react-router-dom") as typeof Dom),
    useLocation: () => ({
        search: "localhost:3000/updatepassword/?id=dba4043c-7428-4f72-ba13-fe782c7a88fa",
    }),
}));

describe("test account group component: updatePassword", () => {
    const testContent = {
        language: "eng",
        caption: "Update Password",
        button: "Submit",
        labelNewPassword: "New password",
        labelVerifyPassword: "Verify new password",
    };

    let data = ApplicationDefault;
    data.contentUpdatePassword.isLoading = false;
    data.contentUpdatePassword.content = testContent;

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(
        <div>
            <UpdatePassword />
        </div>
    );

    beforeEach(() => {
        useSelectorMock.mockClear();
        useDispatchMock.mockClear();
        wrapper.find("UpdatePassword").dive();
    });

    it("should render correctly '<UpdatePassword />' when content is loaded.", () => {
        useDispatchMock.mockReturnValue(jest.fn());
        useSelectorMock.mockReturnValue(data);

        expect(useDispatchMock).toBeCalledTimes(1);
        expect(wrapper).toMatchSnapshot();
    });
});
