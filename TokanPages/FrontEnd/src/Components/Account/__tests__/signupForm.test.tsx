import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SignupForm from "../signupForm";

describe("Test account component: SignupForm.", () => 
{

    it("Renders correctly '<SignupForm />' ", () => 
    {
        const tree = shallow(<SignupForm/>);
        expect(tree).toMatchSnapshot();
    });

});
