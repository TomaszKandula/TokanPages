import React from "react";
import { ProcessedExperienceItemProps } from "../../Types";
import { Icon, Link } from "../../../../Shared/Components";
import Validate from "validate.js";

export const RenderCompanyLink = (props: ProcessedExperienceItemProps): React.ReactElement =>
    Validate.isEmpty(props.companyLink) ? (
        <Icon name="OpenInNew" size={1.2} className="has-text-grey" />
    ) : (
        <Link to={props.companyLink}>
            <Icon name="OpenInNew" size={1.2} className="has-text-link is-clickable" />
        </Link>
    );
