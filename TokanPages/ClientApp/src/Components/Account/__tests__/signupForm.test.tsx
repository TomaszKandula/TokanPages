import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SignupForm from "../signupForm";
import { signupFormDefault } from "../../../Api/Defaults";

describe("Test account component: SignupForm.", () => 
{
    it("Renders correctly '<SignupForm />' ", () => 
    {
        const tree = shallow(<SignupForm content={signupFormDefault.content} />);
        expect(tree).toMatchSnapshot();
    });
});
