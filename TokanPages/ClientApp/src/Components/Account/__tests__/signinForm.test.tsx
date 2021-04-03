import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SigninForm from "../signinForm";
import { signinFormContentDefault } from "../../../Api/Defaults";

describe("Test account component: SigninForm.", () => 
{
    it("Renders correctly '<SigninForm />' when content is loaded.", () => 
    {
        const tree = shallow(<SigninForm signinForm={signinFormContentDefault} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
