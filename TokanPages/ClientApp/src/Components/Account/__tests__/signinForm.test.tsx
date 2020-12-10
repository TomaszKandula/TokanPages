import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SigninForm from "../signinForm";

describe("Test account component: SigninForm.", () => 
{

    it("Renders correctly '<SigninForm />' ", () => 
    {
        const tree = shallow(<SigninForm />);
        expect(tree).toMatchSnapshot();
    });

});
