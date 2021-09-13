import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import { shallow } from "enzyme";
import UserSignin from "../userSignin";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

jest.mock("react-router", () => 
({
    ...jest.requireActual("react-router") as typeof Router,
    useHistory: () => (jest.fn())
}));

describe("Test account group component: userSignin.", () => 
{
    const testContent = 
    {
        caption: "Sign in",
        button: "Sign in",
        link1: "Don't have an account?",
        link2: "Forgot password?"
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(<div><UserSignin content={testContent} isLoading={false}/></div>);

    beforeEach(() => 
    {
        useSelectorMock.mockClear();
        useDispatchMock.mockClear();
        wrapper.find("UserSignin").dive();
    });
    
    it("Renders correctly '<UserSignin />' when content is loaded.", () => 
    {
        useDispatchMock.mockReturnValue(jest.fn());
        useSelectorMock.mockReturnValue(combinedDefaults);
    
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(wrapper).toMatchSnapshot();
    });
});
