import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ResetForm from "../resetForm";
import { resetFormDefault } from "../../../Api/Defaults";

describe("Test account component: ResetForm.", () => 
{
    it("Renders correctly '<ResetForm />' ", () => 
    {
        const tree = shallow(<ResetForm content={resetFormDefault.content} />);
        expect(tree).toMatchSnapshot();
    });
});
