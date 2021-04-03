import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SignupForm from "../signupForm";
import { signupFormContentDefault } from "../../../Api/Defaults";

describe("Test account component: SignupForm.", () => 
{
    it("Renders correctly '<SignupForm />' when content is loaded.", () => 
    {
        const tree = shallow(<SignupForm signupForm={signupFormContentDefault} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
