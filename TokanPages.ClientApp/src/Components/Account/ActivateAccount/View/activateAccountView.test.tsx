import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { ActivateAccountView } from "./activateAccountView";

describe("Test account group component: activateAccountView.", () => 
{
    it("Renders correctly '<ActivateAccountView />' when content is loaded.", () => 
    {
        const tree = shallow(<ActivateAccountView bind=
        {{
            isLoading: false,
            caption: "Account Activation",
            text1: "Your account has been successfully activated!",
            text2: "You can now sign in.",
            buttonHandler: jest.fn(),
            buttonDisabled: false,
            progress: false,
            buttonText: "Go to main"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
