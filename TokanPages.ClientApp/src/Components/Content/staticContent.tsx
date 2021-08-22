import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Validate from "validate.js";
import { ITextObject } from "../../Shared/Components/ContentRender/Models/textModel";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators, TRequestContent, REQUEST_POLICY, REQUEST_STORY, REQUEST_TERMS } from "../../Redux/Actions/Content/getStaticContentAction";
import StaticContentView from "./staticContentView";

const StaticContent = (props: { content: TRequestContent }): JSX.Element => 
{
    const dispatch = useDispatch();
    const [ data, setData ] = React.useState<ITextObject>({ items: [] });
    const getStaticContentState = useSelector((state: IApplicationState) => state.getStaticContent);

    const fetchData = React.useCallback(async () => 
    {
        switch(props.content)
        {
            case REQUEST_STORY: 
                if (Validate.isEmpty(getStaticContentState.story.items) && !getStaticContentState.story.isLoading)
                {
                    dispatch(ActionCreators.getStoryContent());
                    return;
                }
                setData(getStaticContentState.story);
            break;

            case REQUEST_TERMS: 
                if (Validate.isEmpty(getStaticContentState.terms.items) && !getStaticContentState.terms.isLoading)
                {
                    dispatch(ActionCreators.getTermsContent());
                    return;
                }
                setData(getStaticContentState.terms);
            break;
    
            case REQUEST_POLICY: 
                if (Validate.isEmpty(getStaticContentState.policy.items) && !getStaticContentState.policy.isLoading)
                {
                    dispatch(ActionCreators.getPolicyContent());
                    return;
                }
                setData(getStaticContentState.policy);
            break;
        }
    }, 
    [ dispatch, getStaticContentState, props.content ]);

    React.useEffect(() => { fetchData() }, [ data.items.length, fetchData ]);

    return (<StaticContentView bind=
    {{
        data: data,
        isLoading: Validate.isEmpty(data)
    }}/>);
}

export default StaticContent;
