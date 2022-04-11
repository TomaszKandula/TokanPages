import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ResetPasswordView from "../resetPasswordView";
import { RESET_FORM } from "../../../Shared/constants";

describe("Test account group component: resetPasswordView.", () => 
{
    it("Renders correctly '<ResetPasswordView />' when content is loaded.", () => 
    {
        const tree = shallow(<ResetPasswordView bind=
        {{
            isLoading: false,
            progress: false,
            caption: RESET_FORM,
            button: "Reset",
            email: "tokan@dfds.com",
            formHandler: jest.fn(),
            buttonHandler: jest.fn(),
            labelEmail: "Email address"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
