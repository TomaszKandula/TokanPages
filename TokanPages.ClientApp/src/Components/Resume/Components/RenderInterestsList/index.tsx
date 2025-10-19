import React from "react";
import { LinkDto } from "../../../../Api/Models";
import { Link, Skeleton } from "../../../../Shared/Components";
import { ResumeViewProps } from "../../Types";
import { v4 as uuid } from "uuid";

export const RenderInterestsList = (props: ResumeViewProps): React.ReactElement => (
    <div className="bulma-tags mt-4 mb-6">
        <Skeleton isLoading={props.isLoading} height={24} className="m-2">
            {props.page.resume.interests.list.map((value: LinkDto, _index: number) => (
                <Link to={value.href} key={uuid()} className="bulma-tag bulma-is-medium bulma-is-info bulma-is-light">
                    <>{value.text}</>
                </Link>
            ))}
        </Skeleton>
    </div>
);
