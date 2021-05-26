import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ResetForm from "../resetForm";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account component: ResetForm.", () => 
{
    it("Renders correctly '<ResetForm />' when content is loaded.", () => 
    {
        const tree = shallow(<ResetForm resetForm={combinedDefaults.getResetFormContent} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
