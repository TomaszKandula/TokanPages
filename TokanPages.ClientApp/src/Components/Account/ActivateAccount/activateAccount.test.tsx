import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import * as Router from "react-router";
import * as Dom from "react-router-dom";
import { shallow } from "enzyme";
import { ActivateAccount } from "./activateAccount";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

jest.mock("react-router", () => 
({
    ...jest.requireActual("react-router") as typeof Router,
    useHistory: () => (jest.fn())
}));

jest.mock("react-router-dom", () => 
({
    ...jest.requireActual("react-router-dom") as typeof Dom,
    useLocation: () => 
    ({
      search: "localhost:3000/accountactivation/?id=dba4043c-7428-4f72-ba13-fe782c7a88fa"
    })
}));

describe("Test account group component: activateAccount.", () => 
{
    const testId = "dba4043c-7428-4f72-ba13-fe782c7a88fa";
    const testContent = 
    {
        language: "eng",
        onProcessing:
        {
            type: "Processing",
            caption: "Account Activation",
            text1: "Processing your account..., please wait.",
            text2: "",
            button: ""
        },
        onSuccess: 
        {
            type: "Success",
            caption: "Account Activation",
            text1: "Your account has been successfully activated!",
            text2: "You can now sign in.",
            button: "Go to main"
        },
        onError: 
        {
            type: "Error",
            caption: "Account Activation",
            text1: "Could not activate your account.",
            text2: "Please contact IT support.",
            button: "Retry"
        }
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    const useSelectorMock = jest.spyOn(Redux, "useSelector");
    const wrapper = shallow(<div><ActivateAccount id={testId} content={testContent} isLoading={false} /></div>);

    beforeEach(() => 
    {
        useSelectorMock.mockClear();
        useDispatchMock.mockClear();
        wrapper.find("ActivateAccount").dive();
    });

    it("Renders correctly '<ActivateAccount />' when content is loaded.", () => 
    {
        useDispatchMock.mockReturnValue(jest.fn());
        useSelectorMock.mockReturnValue(combinedDefaults);
    
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(wrapper).toMatchSnapshot();
    });
});
