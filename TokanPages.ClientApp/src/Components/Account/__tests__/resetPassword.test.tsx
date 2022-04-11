import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { shallow } from "enzyme";
import ResetPassword from "../resetPassword";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

jest.mock("react-router", () => 
({
    ...jest.requireActual("react-router") as typeof Router,
}));

describe("Test account group component: resetPassword.", () => 
{
    const testContent = 
    {
        caption: "Reset Password",
        button: "Reset",
        labelEmail: "Email address"
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(<div><ResetPassword content={testContent} isLoading={false} /></div>);

    beforeEach(() => 
    {
        useSelectorMock.mockClear();
        useDispatchMock.mockClear();
        wrapper.find("ResetPassword").dive();
    });

    it("Renders correctly '<ResetPassword />' when content is loaded.", () => 
    {
        useDispatchMock.mockReturnValue(jest.fn());
        useSelectorMock.mockReturnValue(combinedDefaults);
    
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(wrapper).toMatchSnapshot();
    });
});
