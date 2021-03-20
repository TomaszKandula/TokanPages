import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SigninForm from "../signinForm";
import { signinFormDefault } from "../../../Api/Defaults";

describe("Test account component: SigninForm.", () => 
{
    it("Renders correctly '<SigninForm />' when content is loaded.", () => 
    {
        const tree = shallow(<SigninForm signinForm={signinFormDefault} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
