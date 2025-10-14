import * as React from "react";
import { Card, Icon } from "../../../../Shared/Components";
import { TColour } from "../../../../Shared/Types";
import { AccountActivateViewProps } from "../Types";

const ProblemIcon = <Icon name="Alert" size={4.5} />;
const AlertIcon = <Icon name="AlertCircle" size={4.5} />;
const CheckIcon = <Icon name="Check" size={4.5} />;
const InfoIcon = <Icon name="Information" size={4.5} />;

export const AccountActivateView = (props: AccountActivateViewProps): React.ReactElement => {
    let icon;
    let colour;
    if (props.hasSuccess) {
        icon = CheckIcon;
        colour = "has-text-success";
    } else if (props.hasError) {
        icon = AlertIcon;
        colour = "has-text-danger";
    } else {
        icon = InfoIcon;
        colour = "has-text-info";
    }

    return (
        <section className={props.className}>
            <div className="bulma-container bulma-is-max-desktop">
                <div className="py-6">
                    {props.shouldFallback ? (
                        <Card
                            isLoading={props.isLoading}
                            caption={props.fallback?.caption}
                            text={props.fallback?.text}
                            icon={ProblemIcon}
                            colour="has-text-warning"
                        />
                    ) : (
                        <Card
                            isLoading={props.isLoading}
                            caption={props.caption}
                            text={[props.text1, props.text2]}
                            icon={icon}
                            colour={colour as TColour}
                        />
                    )}
                </div>
            </div>
        </section>
    );
};
