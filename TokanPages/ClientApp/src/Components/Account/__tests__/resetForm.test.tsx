import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ResetForm from "../resetForm";
import { resetFormContentDefault } from "../../../Api/Defaults";

describe("Test account component: ResetForm.", () => 
{
    it("Renders correctly '<ResetForm />' when content is loaded.", () => 
    {
        const tree = shallow(<ResetForm resetForm={resetFormContentDefault} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
