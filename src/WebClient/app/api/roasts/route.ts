import { NextRequest, NextResponse } from 'next/server';

const baseUrl = process.env.MOCKINGMIRROR_API_BASEURL;

export async function POST(request: NextRequest) {
  try {
    const body = await request.json();

    const response = await fetch(`${baseUrl}/api/roasts`, {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'POST,OPTIONS',
      },
      body: JSON.stringify(body),
    });

    if (!response.ok) {
      return NextResponse.json(
        { error: 'Failed to create roast' },
        { status: response.status }
      );
    }

    const data = await response.json();
    return NextResponse.json(data);
  } catch (error) {
    console.error('Error creating roast:', error);
    return NextResponse.json(
      { error: 'Internal server error' },
      { status: 500 }
    );
  }
}
