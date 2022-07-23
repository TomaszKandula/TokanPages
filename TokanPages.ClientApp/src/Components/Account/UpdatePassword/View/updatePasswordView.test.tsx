import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { UpdatePasswordView } from "./updatePasswordView";
import { UPDATE_FORM } from "../../../../Shared/constants";

describe("Test account group component: updatePasswordView.", () => 
{
    it("Renders correctly '<UpdatePasswordView />' when content is loaded.", () => 
    {
        const tree = shallow(<UpdatePasswordView bind=
        {{
            isLoading: false,
            progress: false,
            caption: UPDATE_FORM,
            button: "Submit",
            newPassword: "user1password",
            verifyPassword: "user1password",
            formHandler: jest.fn(),
            buttonHandler: jest.fn(),
            disableForm: false,
            labelNewPassword: "New password",
            labelVerifyPassword: "Verify password"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
