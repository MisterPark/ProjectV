
void MainLight_half(float3 WorldPos, out half3 Direction, out half3 Color, out half DistanceAtten, out half ShadowAtten)
{
#ifdef SHADERGRAPH_PREVIEW			//! 쉐이더 그래프 프리뷰에서 보이는 결과
	Direction = half3(0.5, 0.5, 0);
	Color = half3(0, 0, 0);
	DistanceAtten = 1;
	ShadowAtten = 1;
#else

#ifdef SHADOWS_SCREEN	//! Screen Space Shadow 사용할때 (No확실)
	half4 clipPos = TransformWorldToHClip(WorldPos);
	half4 shadowCoord = ComputeScreenPos(clipPos);
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);	//! 월드 공간좌표를 그림자 공간 좌표로 변환하는 함수 (No확실)
#endif

	Light mainLight = GetMainLight();			//! Lighting.hlsl 함수로 라이트 정보 및 쉐도우 데이터 구조체 얻어옴
	Direction = normalize(mainLight.direction);	//! 메인 라이트 벡터
	Color = mainLight.color;				//! 메인 라이트 칼라

	DistanceAtten = mainLight.distanceAttenuation;	//! 컬링마스크에 의해 컬링되면 1, 아니면 0 (No확실), 라이트맵 상황때는 다름
	//shadowAtten = mainLight.shadowAttenuation;		//! <ㅗ
	ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();	//! 쉐도우 감쇠값
	half4 shadowParams = GetMainLightShadowParams();
	ShadowAtten = SampleShadowmap(TEXTURE2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), TransformWorldToShadowCoord(WorldPos), shadowSamplingData, shadowParams, false);
#endif
}