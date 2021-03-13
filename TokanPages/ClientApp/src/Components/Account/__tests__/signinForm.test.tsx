import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SigninForm from "../signinForm";
import { signinFormDefault } from "../../../Api/Defaults";

describe("Test account component: SigninForm.", () => 
{
    it("Renders correctly '<SigninForm />' ", () => 
    {
        const tree = shallow(<SigninForm content={signinFormDefault.content} />);
        expect(tree).toMatchSnapshot();
    });
});
