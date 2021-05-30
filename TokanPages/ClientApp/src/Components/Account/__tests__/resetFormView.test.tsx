import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import ResetFormView from "../resetFormView";
import { combinedDefaults } from "../../../Redux/combinedDefaults";

describe("Test account component: ResetForm.", () => 
{
    it("Renders correctly '<ResetFormView />' when content is loaded.", () => 
    {
        const tree = shallow(<ResetFormView content={combinedDefaults.getResetFormContent.content} isLoading={false} />);
        expect(tree).toMatchSnapshot();
    });
});
