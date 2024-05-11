import {InProgress} from '../components/inprogress';
import React, { useEffect, useState } from 'react';


interface Employee {
    id: number;
    name: string;
    score: number;
}

export const Rating = () => <InProgress/>;