import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ResetForm from "../resetForm";

describe("Test account component: ResetForm.", () => 
{

    it("Renders correctly '<ResetForm />' ", () => 
    {
        const tree = shallow(<ResetForm/>);
        expect(tree).toMatchSnapshot();
    });

});
