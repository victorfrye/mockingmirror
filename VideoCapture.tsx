'use client';

import { useEffect, useRef, useState } from 'react';

interface VideoCaptureProps {
  width?: string;
  height?: string;
}

const VideoCapture = ({ width = '100%', height = 'auto' }: VideoCaptureProps) => {
  const videoRef = useRef<HTMLVideoElement>(null);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    const setupCamera = async () => {
      try {
        const stream = await navigator.mediaDevices.getUserMedia({ 
          video: true,
          audio: false
        });
        
        if (videoRef.current) {
          videoRef.current.srcObject = stream;
        }
      } catch (err) {
        setError('Unable to access camera');
        console.error('Error accessing camera:', err);
      }
    };

    setupCamera();

    return () => {
      // Cleanup: stop all tracks when component unmounts
      if (videoRef.current?.srcObject) {
        const tracks = (videoRef.current.srcObject as MediaStream).getTracks();
        tracks.forEach(track => track.stop());
      }
    };
  }, []);

  return (
    <video
      ref={videoRef}
      autoPlay
      playsInline
      muted
      width={width}
      height={height}
    >
      {error || 'Video not supported.'}
    </video>
  );
};

export default VideoCapture;