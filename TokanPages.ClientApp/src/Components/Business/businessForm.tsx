import * as React from "react";
import { BusinessFormView } from "./View/businessFormView";

export interface BusinessFormProps {
    pt?: number;
    pb?: number;
    hasCaption?: boolean;
    hasIcon?: boolean;
    hasShadow?: boolean;
    background?: React.CSSProperties;
}

//TODO: add logic
export const BusinessForm = (props: BusinessFormProps): JSX.Element => {
    return(
        <BusinessFormView
            isLoading={false}
            caption="Business Inquiry"
            text=""
            keyHandler={() => {}}
            formHandler={() => {}}
            firstName=""
            lastName=""
            email=""
            subject=""
            message=""
            buttonHandler={() => {}}
            progress={false}
            buttonText="Submit"
            consent=""
            labelFirstName=""
            labelLastName=""
            labelEmail=""
            labelSubject=""
            labelMessage=""
            multiline={true}
            minRows={6}
            pt={props.pt}
            pb={props.pb}
            background={props.background}
            hasIcon={props.hasIcon}
            hasCaption={props.hasCaption}
            hasShadow={props.hasShadow}
        />
    );
};
